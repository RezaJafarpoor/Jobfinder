﻿using Jobfinder.Domain.Entities;
using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Application.Dtos.JobOffer;

public record CreateJobOfferDto
    (
        string JobName,
        string JobDescription,
        JobDetails JobDetails,
        Salary Salary,
        string CompanyName,
        JobCategory JobCategory
        );