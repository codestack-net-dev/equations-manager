using System;

namespace Xarial.Community.EqMgr.Core.Services
{
    /// <summary>
    /// This service resolves the variable into the calculated value
    /// </summary>
    /// <remarks>This service is coupled with <see cref="IVariable"/></remarks>
    public interface IVariableResolver
    {
        /// <summary>
        /// Type of the variable (<see cref="IVariable"/>) this server resolves
        /// </summary>
        Type VariableType { get; }

        /// <summary>
        /// Returns the value of the variable
        /// </summary>
        /// <param name="var">Variable to resolve</param>
        /// <returns>Resolved value</returns>
        /// <remarks>The passed variable would be of type set in <see cref="VariableType"/> property</remarks>
        string Resolve(IVariable var);

        /// <summary>
        /// Attempts to create a variable from the text content
        /// </summary>
        /// <param name="content">String content of the variable</param>
        /// <param name="var">Instance of the variable. See remarks section of <see cref="IVariable"/> interface regarding the pointers</param>
        /// <returns>True if the content matches the variable</returns>
        /// <remarks>This is a probing method which will be called by <see cref="IVariableResolver"/> to check if the variable to find the variable instance</remarks>
        bool TryCreate(string content, out IVariable var);
    }
}
