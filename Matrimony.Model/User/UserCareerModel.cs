﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Matrimony.Model.User
{
    public class UserCareerModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? WorkingSectorId { get; set; }
        public int? WorkDesignationId { get; set; }
        public int? EmployerId { get; set; }
        public int? AnualIncomeId { get; set; }
        public bool? IsDisplayIncome { get; set; }
    }
}
