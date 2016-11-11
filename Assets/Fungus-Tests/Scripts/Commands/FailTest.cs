﻿// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{
    [CommandInfo("Tests",
                 "Fail",
                 "Fails the current integration test")]
    [AddComponentMenu("")]
    public class FailTest : Command
    {
        public string failMessage;

        public override void OnEnter ()
        {
            IntegrationTest.Fail(failMessage);

            Continue();
        }
    }
}