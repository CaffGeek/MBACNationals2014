using System;
using System.Runtime.Serialization;

namespace MBACNationals.Team
{
    public class TeamAlreadyExists : Exception, ISerializable
    {
        public TeamAlreadyExists()
        {
            // Add implementation.
        }
        public TeamAlreadyExists(string message)
        {
            // Add implementation.
        }
        public TeamAlreadyExists(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected TeamAlreadyExists(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
