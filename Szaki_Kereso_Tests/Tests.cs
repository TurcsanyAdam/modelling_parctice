using NUnit.Framework;
using System;
using System.Collections.Generic;
using Szaki_kereso;

namespace Szaki_Kereso_Tests
{
    public class Tests
    {

        User user;
        Handyman handyman1;
        Handyman handyman2;

        [SetUp]
        public void Setup()
        {

            user = new User("Turi", "Turcs�ny", "�d�m", 25, "turcsanyadam@gmail.com", "J�sika+M+43");
            handyman1 = new Handyman("laci01", "Kov�cs", "L�szl�", 30, "kovacslaszlo@gmail.com", "Gy�ri+Kapu+160", "electrician");
            handyman2 = new Handyman("laci02", "Kov�cs", "L�szl�", 31, "kovacslaszlo2@gmail.com", "Gy�ri+Kapu+161", "engineer");
            
        }

        [Test]
        public void TopUpBalanceWithPositiveNumber()
        {
            user.AddMoney(100);
            Assert.AreEqual(100, user.Money);
        }


        [Test]
        public void TopUpBalanceWithNegativeNumber()
        {
            user.AddMoney(-100);
            Assert.AreEqual(100, user.Money);
        }

        [Test]
        public void WorkWithHandymanWithMoney()
        {
            handyman1.WorkingFee = 100;
            user.AddMoney(200);
            handyman1.Work(user);
            Assert.AreEqual(100, user.Money);
            Assert.AreEqual(100, handyman1.Money);
        }

        [Test]
        public void WorkWithHandymanWithoutMoney()
        {
            handyman1.WorkingFee = 100;
            var ex = Assert.Throws<NoMoneyForWorkException>(() => handyman1.Work(user));
            Assert.That(ex.Message, Is.EqualTo($"Not enought money for this job to work with {handyman1.Username}"));

        }

    }
}