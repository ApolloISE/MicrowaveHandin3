using System;
using System.Reflection;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using Microwave.Classes.Boundary;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new Buzzer(output);
        }
         
        [Test]
        public void CookingIsDoneOutputIsCalledThrice()
        {
            uut.CookingIsDone();
            output.ReceivedWithAnyArgs(3).OutputLine(default);
        }
    }
}