using System;
using JoesPetStore.Models;

namespace JoesPetStore.ViewModels
{
    public class ApprovalViewModel
    {
        public String CustomerEmail { get; set; }
        public ApprovalState ApprovalState { get; set; }
    }
}
