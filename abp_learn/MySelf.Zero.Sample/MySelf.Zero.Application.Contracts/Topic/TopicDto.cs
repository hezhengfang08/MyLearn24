﻿using MySelf.Zero.Application.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Topic
{
    public class TopicDto
    {
        public long Id { get; set; }

        public int UserId { get; set; }

        public string TopicName { get; set; }

        public string TopicContent { get; set; }

        public int Views { get; set; }

        public int FavouriteTimes { get; set; }

        public int? LastReciveUserId { get; set; }

        public int Commits { get; set; }

        public DateTime? LastReciveTime { get; set; }
        public UserDto User { get; set; }
    }
}
