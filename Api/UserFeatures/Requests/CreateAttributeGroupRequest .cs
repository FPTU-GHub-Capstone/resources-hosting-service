﻿using DomainLayer.Entities;
using System.ComponentModel.DataAnnotations;
using WebApiLayer.Mappings;

namespace WebApiLayer.UserFeatures.Requests;

public class CreateAttributeGroupRequest : IMapTo<AttributeGroupEntity>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Effect { get; set; }
}