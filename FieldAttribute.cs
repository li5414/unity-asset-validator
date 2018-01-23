/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;
using UnityEngine;

namespace JCMG.AssetValidator
{
    /// <summary>
    /// A FieldAttribute targets a public instance field and is intended to have a matching 
    /// FieldValidator subclass that is designed to validate that field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldAttribute : PropertyAttribute
    {
    }
}
