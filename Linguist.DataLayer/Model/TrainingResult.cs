﻿using System;

namespace Linguist.DataLayer.Model
{
    public class TrainingResult
    {
        public int TrainingResultId { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public string Words { get; set; }
    }
}
