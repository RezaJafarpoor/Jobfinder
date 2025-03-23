﻿namespace Jobfinder.Domain.Entities;

public class EmployerProfile 
{
    public Guid Id { get; set; }
    public Company Company { get; set; } = new();
    public IList<JobOffer> JobOffers { get; set; } = [];
    public User User { get; set; } = new();
    public Guid UserId { get; set; }
}