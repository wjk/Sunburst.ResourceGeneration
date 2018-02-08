using System;
using Microsoft.Build.Utilities;

namespace Sunburst.ResourceGeneration
{
    public abstract class TaskBase : Task
    {
        public sealed override bool Execute()
        {
            try
            {
                ExecuteCore();
                return true;
            }
            catch (BuildErrorException ex)
            {
                Log.LogError(ex.Message);
                return false;
            }
        }

        protected abstract void ExecuteCore();
    }

    public sealed class BuildErrorException : Exception
    {
        public BuildErrorException() : this("") { }
        public BuildErrorException(string text) : base(text) { }
        public BuildErrorException(string format, params object[] args) : this(string.Format(format, args)) { }
    }
}
