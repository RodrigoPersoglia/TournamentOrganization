﻿namespace TournamentOrganization.BusinessLogic.Helpers
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
