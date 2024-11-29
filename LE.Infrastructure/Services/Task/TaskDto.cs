﻿using System.ComponentModel.DataAnnotations;

namespace LE.Infrastructure.Services.Task
{
    public class TaskDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        public Guid UserId { get; set; } = default!;

        [MaxLength(300)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public Priority Priority { get; set; } = Priority.Low;

        public Status Status { get; set; } = Status.None;
    }
}