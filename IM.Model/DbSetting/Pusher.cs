using SP.StudioCore.Gateway.Push;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Model.DbSetting
{
    public sealed class Pusher : GoEasy
    {
        public Pusher() : base(Setting.Pusher)
        {
        }
    }
}
