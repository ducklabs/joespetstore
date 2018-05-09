using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FluentAssert;
using JoesPetStore.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace JoesPetStore.Tests.Functional
{
    [TestFixture]
    class PetStoreTests
    {

        private static PetInputViewModel CreatePetInputViewModel()
        {
            return new PetInputViewModel { Name = "Leo" };
        }

        private static PetDetailsViewModel CreateAnonymousPet()
        {
            var petInputViewModel = CreatePetInputViewModel();
            Facade.CreatePet(petInputViewModel);
            return Facade.FindPet();
        }

        [TearDown]
        public void TearDown()
        {
            using (var context = new PetDbContext())
            {
                context.Database.ExecuteSqlCommand(
                    @"exec sp_MSforeachtable @precommand = null
                    ,@command1 = 'TRUNCATE TABLE ?'
                    ,@command2 = null
                    ,@postcommand = null
                    ,@whereand = 'AND schema_name(schema_id) = ''dbo'' and type_desc = ''USER_TABLE'' and object_name(object_id) not in (''__MigrationHistory'')'"
                );
            }
        }

        [Test]
        public void TestCreatePet()
        {
            // assemble
            var expectedPerDetailsViewModel = CreatePetInputViewModel();

            // act
            Facade.CreatePet(expectedPerDetailsViewModel);

            // assert
            var actualPetDetaislViewModel = Facade.FindPet();
            actualPetDetaislViewModel.Name.ShouldBeEqualTo(expectedPerDetailsViewModel.Name);
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
        public void TestPurchasePet()
        {
            // assemble
            var expectedPetViewModel = CreateAnonymousPet();

            // act
            Facade.PurchasePet(expectedPetViewModel.Id);

            // assert
            var receiptViewModel = Facade.FindPurchaseReceipt(expectedPetViewModel.Id);
            receiptViewModel.PetId.ShouldBeEqualTo(expectedPetViewModel.Id);

        }


        [Test]
        public void TestPurchasePet_PetAlreadyPurchased()
        {
            // assemble
            var expectedPetViewModel = CreateAnonymousPet();
            Facade.PurchasePet(expectedPetViewModel.Id);

            // act
            Facade.PurchasePet(expectedPetViewModel.Id);

            // assert
            var receiptViewModel = Facade.FindPurchaseReceipt(expectedPetViewModel.Id);
            receiptViewModel.PetId.ShouldBeEqualTo(expectedPetViewModel.Id);
        }

        [Test]
        public void TestPurchasePet_NoPets()
        {
            // assemble

            // act
            Facade.PurchasePet(0);

            // assert
            var receiptViewModel = Facade.FindPurchaseReceipt(0);
            receiptViewModel.ShouldBeNull();
        }
    }
}
