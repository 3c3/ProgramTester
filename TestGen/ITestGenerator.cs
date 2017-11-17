using System;

namespace TestGen
{
    public interface ITestGenerator
    {
        string DisplayName
        {
            get;
        }
        string Description
        {
            get;
        }

        string MakeTest();
    }
}
