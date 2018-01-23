﻿/*
unity-asset-validator Copyright (C) 2017  Jeff Campbell

unity-asset-validator is licensed under a
Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.
*/
using UnityEngine;

namespace JCMG.AssetValidator.UnitTestObjects
{
    [Validate]
    public class ValidatedMonobehavior2 : Monobehavior2
    {
        [NonNull]
        public GameObject gameObjectRef;
    }
}