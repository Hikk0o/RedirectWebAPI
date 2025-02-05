﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RedirectAPI.Data.DataEntity;

/// Database Entity
[Table("users")] public class User
{ 
    [Column("id")] public int Id { get; set; }
    [Column("ip")] public string Ip { get; set; } = null!;

    /// <returns>string entity</returns>
    public override string ToString()
    {
        return $"{Id}: {Ip}";
    }
}
