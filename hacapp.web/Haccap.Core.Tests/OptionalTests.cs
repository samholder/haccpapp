using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hacapp.Core.Tests
{
    [TestClass]
    public class OptionalTests
    {
        [TestClass]
        public class TheConstructorMethod : OptionalTests
        {
            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhenConstructedWithNull()
            {
                //Arrange------------------------------------------------

                //Act----------------------------------------------------
                Action action = () => new Optional<Object>(null);
                //Assert-------------------------------------------------
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [TestClass]
        public class TheEqualsMethod : OptionalTests
        {
            [TestMethod]
            public void ShouldBeTrueWhenOptionalsAreSameInstance()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>(1);
                Optional<int> optional2 = optional;
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.Equals(optional2).Should().BeTrue();
            }

            [TestMethod]
            public void ShouldBeTrueWhenOptionalsHaveValuesWhichAreEqual()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>(1);
                var optional2 = new Optional<int>(1);
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.Equals(optional2).Should().BeTrue();
            }

            [TestMethod]
            public void ShouldBeTrueWhenOptionalsHaveNoValue()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>();
                var optional2 = new Optional<int>();
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.Equals(optional2).Should().BeTrue();
            }

            [TestMethod]
            public void ShouldBeFalseWhenOptionalsAreDifferentValues()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>(1);
                var optional2 = new Optional<int>(2);
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.Equals(optional2).Should().BeFalse();
            }
        }


        [TestClass]
        public class TheGetHashCodeMethod : OptionalTests
        {
            [TestMethod]
            public void ShouldBeTrueWhenOptionalsAreSameInstance()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>(1);
                Optional<int> optional2 = optional;
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.GetHashCode().Should().Be(optional2.GetHashCode());
            }

            [TestMethod]
            public void ShouldBeTrueWhenOptionalsHaveValuesWhichAreEqual()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>(1);
                var optional2 = new Optional<int>(1);
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.GetHashCode().Should().Be(optional2.GetHashCode());
            }

            [TestMethod]
            public void ShouldBeFalseWhenOptionalsHaveNoValueAndAreDifferentInstances()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>();
                var optional2 = new Optional<int>();
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.GetHashCode().Should().NotBe(optional2.GetHashCode());
            }

            [TestMethod]
            public void ShouldBeFalseWhenOptionalsAreDifferentValues()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>(1);
                var optional2 = new Optional<int>(2);
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.GetHashCode().Should().NotBe(optional2.GetHashCode());
            }
        }

        [TestClass]
        public class TheHasValueMethod : OptionalTests
        {
            [TestMethod]
            public void ShouldBeFalseWhenDefaultConstructorIsUsed()
            {
                //Arrange
                var optional = new Optional<int>();
                //Act

                //Assert
                optional.HasValue.Should().BeFalse();
            }

            [TestMethod]
            public void ShouldBeTrueIfValueIsPassedToConstructor()
            {
                //Arrange
                var optional = new Optional<int>(1);
                //Act

                //Assert
                optional.HasValue.Should().BeTrue();
            }
        }

        [TestClass]
        public class TheValueMethod : OptionalTests
        {
            [TestMethod]
            public void ShouldReturnValueIfOneExists()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>(1);
                //Act----------------------------------------------------

                //Assert-------------------------------------------------
                optional.Value.Should().Be(1);
            }

            [TestMethod]
            public void ShouldThrowExceptionIfNoValueIsSet()
            {
                //Arrange------------------------------------------------
                var optional = new Optional<int>();
                //Act----------------------------------------------------
                int value;
                Action action = () => value = optional.Value;
                //Assert-------------------------------------------------
                action.ShouldThrow<InvalidOperationException>();
            }
        }
    }
}