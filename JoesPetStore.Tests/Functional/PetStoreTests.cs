using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using FluentAssert;
using JoesPetStore.Exceptions;
using JoesPetStore.Mail;
using JoesPetStore.Models;
using JoesPetStore.ViewModels;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using TransactionManager = JoesPetStore.Models.TransactionManager;

namespace JoesPetStore.Tests.Functional
{
    [TestFixture]
    class PetStoreTests
    {

        private static PetInputViewModel CreatePetInputViewModel()
        {
            return new PetInputViewModel { Name = "Leo" };
        }

        private static PetDetailsViewModel CreateAnonymousPetInDb()
        {
            var petInputViewModel = CreatePetInputViewModel();
            Facade.CreatePet(petInputViewModel);
            return Facade.FindPet();
        }
      
        [SetUp]
        public void Setup()
        {
            TransactionManager.DeleteEntities<Receipt>();
            TransactionManager.DeleteEntities<Pet>();
            TransactionManager.DeleteEntities<Approval>();

            EmailServerServiceFactory.UseEmailServer(new MockEmailServerService());

        }

        [Test]
        public void TestCreatePet()
        {
            // assemble
            var expectedPetDetailsViewModel = CreatePetInputViewModel();

            // act
            Facade.CreatePet(expectedPetDetailsViewModel);

            // assert
            PetDetailsViewModel actualPetDetaislViewModel = Facade.FindPet();
            actualPetDetaislViewModel.Name.ShouldBeEqualTo(expectedPetDetailsViewModel.Name);
        }

        [Test]
        public void TestFindPetDetails_NoPets()
        {
            // assemble

            // act
            var actualPetDetaislViewModel = Facade.FindPet();

            // assert
            actualPetDetaislViewModel.ShouldBeNull();
        }

        [Test]
        public void TestRequestApproval_CreatesPendingApproval()
        {
            // assemble
            var customerEmail = "joe@goodhomesforpets.com";

            // act
            Facade.RequestPetPurchase(customerEmail);

            // assert
            List<ApprovalViewModel> pendingApprovals = Facade.GetPendingApprovals();
            pendingApprovals.Count.ShouldBeEqualTo(1);
            pendingApprovals[0].CustomerEmail.ShouldBeEqualTo(customerEmail);
        }

        [Test]
        public void TestGetPendingApprovals_NoPendingApprovals()
        {
            // assemble

            // act
            List<ApprovalViewModel> pendingApprovals = Facade.GetPendingApprovals();

            // assert
            pendingApprovals.ShouldBeEmpty();
        }

        [Test]
        public void TestGetPendingApprovals()
        {
            // assemble
            var customerEmail = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmail);

            // act
            List<ApprovalViewModel> pendingApprovals = Facade.GetPendingApprovals();

            // assert
            pendingApprovals.Count.ShouldBeEqualTo(1);
            pendingApprovals[0].CustomerEmail.ShouldBeEqualTo(customerEmail);
        }

        [Test]
        public void TestAcceptPendingApproval_CreatesAcceptedApproval()
        {
            // assemble
            var expectedApprovalState = ApprovalState.Approved;
            var customerEmail = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmail);

            // act
            var firstPendingApproval = Facade.GetPendingApprovals().FirstOrDefault();
            Facade.Approve(firstPendingApproval);
            
            // assert
            ApprovalViewModel acceptedApproval = Facade.GetApprovals(ApprovalState.Approved).FirstOrDefault();
            acceptedApproval.ApprovalState.ShouldBeEqualTo(expectedApprovalState);
        }

        [Test]
        public void TestAcceptPendingApproval_FiresAnEmailWithTheApproval()
        {
            // assemble            
            var mockEmailServer = new MockEmailServerService();
            EmailServerServiceFactory.UseEmailServer(mockEmailServer);

            var customerEmail = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmail);

            // act
            var firstPendingApproval = Facade.GetPendingApprovals().FirstOrDefault();
            Facade.Approve(firstPendingApproval);

            // assert
            mockEmailServer.Sent.ShouldBeTrue();
            mockEmailServer.Email.To.ShouldBeEqualTo(customerEmail);
            mockEmailServer.Email.Message.ShouldBeEqualTo("Congratulations, you've been approved and you can purchase Leo <a href='localhost:50112/Pet/PurchasePage?customerEmail=\""+ customerEmail + "\"' >Purchase Pet</a>");
        }

        [Test]
        public void TestGetPendingApprovals_MultiplePendingApprovalsForSamePet()
        {
            // assemble
            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var customerEmailTwo = "joe@discountmeats.com";
            Facade.RequestPetPurchase(customerEmailTwo);

            // act
            var pendingApprovals = Facade.GetPendingApprovals();

            // assert
            pendingApprovals.Count.ShouldBeEqualTo(2);
            pendingApprovals.FirstOrDefault(app => app.CustomerEmail == customerEmailOne).ShouldNotBeNull();
            pendingApprovals.FirstOrDefault(app => app.CustomerEmail == customerEmailOne).ShouldNotBeNull();
        }

        [Test]
        public void TestGetPendingApprovals_ApprovalForPetHasBeenAccepted_NoLongerShowOtherPendingApprovalsForSamePet()
        {
            // assemble
            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var customerEmailTwo = "joe@discountmeats.com";
            Facade.RequestPetPurchase(customerEmailTwo);

            // act
            var goodHomeApproval = Facade.GetPendingApprovals().FirstOrDefault(app => app.CustomerEmail == customerEmailOne);
            Facade.Approve(goodHomeApproval);

            // assert
            var pendingApprovals = Facade.GetPendingApprovals();
            pendingApprovals.Count.ShouldBeEqualTo(0);
        }


        [Test]
        public void TestRejectApproval_FiresAnEmail()
        {
            // assemble            
            var mockEmailServer = new MockEmailServerService();
            EmailServerServiceFactory.UseEmailServer(mockEmailServer);

            var customerEmail = "joe@discountmeats.com";
            Facade.RequestPetPurchase(customerEmail);

            // act
            var firstPendingApproval = Facade.GetPendingApprovals().FirstOrDefault();
            Facade.Reject(firstPendingApproval);

            // assert
            mockEmailServer.Sent.ShouldBeTrue();
            mockEmailServer.Email.To.ShouldBeEqualTo(customerEmail);
            mockEmailServer.Email.Message.ShouldBeEqualTo("Sorry, you have been denied.");
        }

        [Test]
        public void TestRejectApproval_AlreadyApproved_Fails()
        {
            Assert.Fail("Unimplemented");
        }

        [Test]
        public void TestApproveApproval_AlreadyDenied_Fails()
        {
            Assert.Fail("Unimplemented");
        }

        [Test]
        public void TestRejectApproval_RemovesPendingApproval()
        {
            // assemble
            var customerEmailTwo = "joe@discountmeats.com";
            Facade.RequestPetPurchase(customerEmailTwo);

            // act
            var badHomeApproval = Facade.GetPendingApprovals().FirstOrDefault(app => app.CustomerEmail == customerEmailTwo);
            Facade.Reject(badHomeApproval);

            // assert
            var pendingApprovals = Facade.GetPendingApprovals();
            pendingApprovals.Count.ShouldBeEqualTo(0);
        }

        [Test]
        public void TestRejectApproval_LeavesOtherPendingApprovalsUntouched()
        {
            // assemble
            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var customerEmailTwo = "joe@discountmeats.com";
            Facade.RequestPetPurchase(customerEmailTwo);

            // act
            var badHomeApproval = Facade.GetPendingApprovals().FirstOrDefault(app => app.CustomerEmail == customerEmailTwo);
            Facade.Reject(badHomeApproval);

            // assert
            var pendingApprovals = Facade.GetPendingApprovals();
            pendingApprovals.Count.ShouldBeEqualTo(1);
            pendingApprovals.FirstOrDefault(app => app.CustomerEmail == customerEmailOne).ShouldNotBeNull();
        }

        /*
        [Test]
        public void TestCustomerPurchase_RejectsOtherPendingApprovalsForSamePet()
        {
            
            // assemble
            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var customerEmailTwo = "joe@discountmeats.com";
            Facade.RequestPetPurchase(customerEmailTwo);

            // act
            var goodHomeApproval = Facade.GetPendingApprovals().FirstOrDefault(app => app.CustomerEmail == customerEmailOne);
            Facade.Approve(goodHomeApproval);

            var approvedApproval = Facade.GetApprovals(ApprovalState.Approved).FirstOrDefault();
            Facade.PurchasePet(approvedApproval);

            // assert
            Facade.GetApprovals(ApprovalState.Pending).Count.ShouldBeEqualTo(0);
            
            Assert.Fail("Entity framework Receipt Foreign Key Constraint Problem");

        }*/

        [Test]
        public void TestRequestApproval_PetAlreadyPurchased_Fails()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");

        }

        [Test]
        public void TestPurchasePet_WithApproval()
        {
            // assemble
            var expectedPetViewModel = CreateAnonymousPetInDb();
            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var firstPendingApproval = Facade.GetPendingApprovals().FirstOrDefault();
            Facade.Approve(firstPendingApproval);

            var approvedApproval = Facade.GetApprovals(ApprovalState.Approved).FirstOrDefault();

            // act
            Facade.PurchasePet(approvedApproval);

            // assert
            var receiptViewModel = Facade.FindPurchaseReceipt();
            receiptViewModel.PetId.ShouldBeEqualTo(expectedPetViewModel.Id);
        }

        [Test]
        public void TestUserApprovalCancel_PetReappearsInGetPendingApprovals()
        {
            /*
            // assemble
            var expectedPetViewModel = CreateAnonymousPetInDb();
            var approvedApproval = Facade.GetApprovals(ApprovalState.Approved).FirstOrDefault();

            // act
            Facade.PurchasePet(approvedApproval);

            // assert
            var receiptViewModel = Facade.FindPurchaseReceipt();
            receiptViewModel.PetId.ShouldBeEqualTo(expectedPetViewModel.Id);*/
            Assert.Fail("Unimplemented");
        }

        [Test]
        public void TestPurchasePet_WithoutApproval_Fails()
        {
            // assemble
            var expectedPetViewModel = CreateAnonymousPetInDb();
            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var firstPendingApproval = Facade.GetPendingApprovals().FirstOrDefault();
            Facade.Reject(firstPendingApproval);

            var rejectedApproval = Facade.GetApprovals(ApprovalState.Denied).FirstOrDefault();

            // Act & Assert
            Assert.Throws<PurchasePetException>( 
                () => Facade.PurchasePet(rejectedApproval)
            );

        }

        private void PurchasePet()
        {
            CreateAnonymousPetInDb();
            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var firstPendingApproval = Facade.GetPendingApprovals().FirstOrDefault();
            Facade.Approve(firstPendingApproval);

            var approvedApproval = Facade.GetApprovals(ApprovalState.Approved).FirstOrDefault();
            Facade.PurchasePet(approvedApproval);
        }

        [Test]
        public void TestApproveApproval_ApprovalAlreadyApproved()
        {

            CreateAnonymousPetInDb();

            var customerEmailOne = "joe@goodhomesforpets.com";
            Facade.RequestPetPurchase(customerEmailOne);

            var customerEmailTwo = "joe@discountmeats.com";
            Facade.RequestPetPurchase(customerEmailTwo);

            var badHomeApproval = Facade.GetPendingApprovals().FirstOrDefault(app => app.CustomerEmail == customerEmailTwo);

            var goodHomeApproval = Facade.GetPendingApprovals().FirstOrDefault(app => app.CustomerEmail == customerEmailOne);
            Facade.Approve(goodHomeApproval);

            // act
            Assert.Throws<ApprovalException>(() => Facade.Approve(badHomeApproval));

        }


    }
}
