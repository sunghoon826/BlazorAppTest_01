﻿using System.ComponentModel.DataAnnotations;

namespace BlazorAppTest_01.Models.Candidates
{
    /// <summary>
    /// 후보자, 지원자(Applicat)
    /// </summary>
    public class CandidateBase
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        public bool IsEnrollment { get; set; }

    }
    public class Candidate : CandidateBase
    {
    }
}
