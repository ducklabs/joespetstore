using System;

namespace JoesPetStore.Exceptions
{
    public class ApprovalException : Exception
    {
        public ApprovalException(string approvalNotInPendingState)
        {
        }
    }
}