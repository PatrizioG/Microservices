﻿namespace Common.Contracts;

public class Category
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Category? Father { get; set; }
}