using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Restaurant;

namespace TestRestaurantCheckOut
{
    [TestClass]
    public class RestaurantUnitTests
    {
        public TestCases tc = new TestCases(new TestData());

        [TestMethod]
        public void AddOrderTest()
        {
            tc.AddOrderTest().Should().BeTrue(); 
        }

        [TestMethod]
        public void UpdateOrderTest()
        {
            tc.UpdateOrderTest().Should().BeTrue();
        }

        [TestMethod]
        public void RemoveOrderTest()
        {
            tc.RemoveOrderTest().Should().BeTrue();
        }

        [TestMethod]
        public void CheckOutTest()
        {
            tc.CheckOutTest().Should().BeTrue();
        }

        [TestMethod]
        public void AddUpdateRemoveCheckOutTest()
        {
            tc.AddUpdateRemoveCheckOutTest().Should().BeTrue();
        }

        [TestMethod]
        public void AddUnavailableFoodToOrderFailTest()
        {
            tc.AddUnavailableFoodOrderTest().Should().BeFalse();
        }

        [TestMethod]
        public void UpdateUnavailableFoodToOrderFailTest()
        {
            tc.UpdateUnavailableFoodOrderTest().Should().BeFalse();
        }

        [TestMethod]
        public void RemoveUnavailableFoodFromOrderFailTest()
        {
            tc.RemoveUnavailableOrderTest().Should().BeFalse();
        }

    }
}
