using System;
using System.ComponentModel.DataAnnotations;
using Exterminator.Models.Exceptions;

namespace Exterminator.Models.Attributes
{


    public class Expertize : ValidationAttribute
    {
            private string[] expertiseList = {"Ghost catcher", "Ghoul strangler",
            "Monster encager", "Zombie exploder"};

            public override bool IsValid(object value)
            {
                var isValid = value is string valueString
                       && Array.Exists(expertiseList, name => name == valueString);
                return isValid;
            }
    }
}