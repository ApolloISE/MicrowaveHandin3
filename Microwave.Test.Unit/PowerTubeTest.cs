using System;
using Castle.DynamicProxy.Generators;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;
        private int defaultPower = 700;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
        }
         
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(699)]
        [TestCase(700)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            uut = new PowerTube(output, defaultPower);
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1201)]
        [TestCase(1300)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power)
        {
            uut = new PowerTube(output, defaultPower);
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut = new PowerTube(output, defaultPower);
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut = new PowerTube(output, defaultPower);
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut = new PowerTube(output, defaultPower);
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }

        [TestCase(50)]
        [TestCase(400)]
        [TestCase(800)]
        [TestCase(900)]
        [TestCase(1200)]
        public void Create_PowerTubeCreated_NoExceptionThrown(int power)
        {
            Assert.DoesNotThrow(() => uut = new PowerTube(output, power));
            //Assert.That(exception, Is.Null);
        }

        [TestCase(49)]
        [TestCase(-500)]
        [TestCase(1201)]
        [TestCase(1500)]
        [TestCase(-1)]
        public void Create_PowerTubeNotCreated_ExceptionThrown(int power)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => uut = new PowerTube(output, power));
            Assert.That(exception, Is.Not.Null);
        }
    }
}