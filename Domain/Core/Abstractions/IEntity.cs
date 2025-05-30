﻿namespace Core.Abstractions;

public interface IEntity<out TId> : IEntity
{
    TId Id { get; }
}

public interface IEntity { }