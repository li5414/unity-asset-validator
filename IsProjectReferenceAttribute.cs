/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using System;

namespace JCMG.AssetValidator
{
    /// <summary>
    /// Instance fields decorated with this attribute must reference on object in the 
    /// project versus the scene, otherwise it will give off a validation error.
    /// </summary>
    public class IsProjectReferenceAttribute : FieldAttribute { }
}

