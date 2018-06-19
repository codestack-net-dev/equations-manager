namespace Xarial.Community.EqMgr.Core
{
    /// <summary>
    /// Structure representing the expression object
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// Name of the expression
        /// </summary>
        /// <remarks>This should represent the key id of the object the expression belongs to (e.g. name of the attribute)</remarks>
        public string Name { get; set; }

        /// <summary>
        /// Formula with placeholders, logical and mathematical expressions
        /// </summary>
        public string Formula { get; set; }
    }
}
