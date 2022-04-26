using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meets.WebApi.Meetup
{
    [Table("meetups")]
    public class MeetupEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("topic")]
        public string Topic { get; set; }
        [Column("place")]
        public string Place { get; set; }
        [Column("duration")]
        public int Duration { get; set; }
    }
}