using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using FluentAssert;
using JoesPetStore.Exceptions;
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
        }

        [Test]
        public void TestCreatePet()
        {
            // assemble
            var expectedPetDetailsViewModel = CreatePetInputViewModel();

            // act
            Facade.CreatePet(expectedPetDetailsViewModel);

            // assert
            var actualPetDetaislViewModel = Facade.FindPet();
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
            List<ApprovalViewModel> pendingApprovals = Facade.GetApprovals(ApprovalState.Pending);
            pendingApprovals.Count.ShouldBeEqualTo(1);
            pendingApprovals[0].CustomerEmail.ShouldBeEqualTo(customerEmail);
        }

        [Test]
        public void TestGetPendingApprovals_NoPendingApprovals()
        {
            // assemble

            // act
            List<ApprovalViewModel> pendingApprovals = Facade.GetApprovals(ApprovalState.Pending);

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
            List<ApprovalViewModel> pendingApprovals = Facade.GetApprovals(ApprovalState.Pending);

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
            Facade.Approve(Facade.GetApprovals(ApprovalState.Pending)[0]);
            
            // assert
            ApprovalViewModel acceptedApproval = Facade.GetApprovals(ApprovalState.Approved)[0];
            acceptedApproval.ApprovalState.ShouldBeEqualTo(expectedApprovalState);
        }

        [Test]
        public void TestAcceptPendingApproval_FiresAnEmailWithTheApproval()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");
        }

        [Test]
        public void TestGetPendingApprovals_ApprovalForPetHasBeenAccepted_NoLongerShowOtherPendingApprovalsForSamePet()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");
        }


        [Test]
        public void TestRejectApproval_FiresAnEmail()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");
        }

        [Test]
        public void TestRejectApproval_RemovesPendingApproval()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");
        }

        [Test]
        public void TestRejectApproval_LeavesOtherPendingApprovalsUntouched()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");
        }

        [Test]
        public void TestCustomerPurchase_RejectsOtherPendingApprovalsForSamePet()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");
        }

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

            // act
            Facade.PurchasePet();

            // assert
            var receiptViewModel = Facade.FindPurchaseReceipt();
            receiptViewModel.PetId.ShouldBeEqualTo(expectedPetViewModel.Id);

        }

        [Test]
        public void TestUserApprovalCancel_PetReappearsInGetPendingApprovals()
        {
            // assemble
            var expectedPetViewModel = CreateAnonymousPetInDb();

            // act
            Facade.PurchasePet();

            // assert
            var receiptViewModel = Facade.FindPurchaseReceipt();
            receiptViewModel.PetId.ShouldBeEqualTo(expectedPetViewModel.Id);

        }

        [Test]
        public void TestPurchasePet_WithoutApproval_Fails()
        {
            // assemble

            // act

            // assert
            Assert.Fail("Unimplemented");

        }


        [Test]
        public void TestPurchasePet_PetAlreadyPurchased()
        {
            // assemble
            CreateAnonymousPetInDb();
            Facade.PurchasePet();

            // Act & Assert
            Assert.Throws<PurchasePetException>( Facade.PurchasePet );
        }

        [Test]
        public void TestPurchasePet_NoPets()
        {
            // Act & Assert
            Assert.Throws<PurchasePetException>( Facade.PurchasePet );
        }

    }
}
