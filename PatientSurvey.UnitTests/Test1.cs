using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace PatientSurvey.UnitTests
{
    public class Test1
    {
        [Fact]
        public void Test()
        {
            "test".Should().Be("1");
        }
    }
}
