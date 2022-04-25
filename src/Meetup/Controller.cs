﻿using System;
using System.Collections.Generic;
using System.Linq;
using Meets.WebApi.Meetup;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/meetups")]
public class MeetupController : ControllerBase
{
    private static readonly ICollection<Meetup> Meetups =
        new List<Meetup>();

    [HttpPost]
    public IActionResult CreateMeetup([FromBody] CreateMeetupDto createDto)
    {
        var newMeetup = new Meetup
        {
            Id = Guid.NewGuid(),
            Topic = createDto.Topic,
            Place = createDto.Place,
            Duration = createDto.Duration
        };
        Meetups.Add(newMeetup);

        var readDto = new ReadMeetupDto
        {
            Id = newMeetup.Id,
            Topic = newMeetup.Topic,
            Place = newMeetup.Place,
            Duration = newMeetup.Duration
        };
        return Ok(readDto);
    }

    [HttpGet]
    public IActionResult GetAllMeetups()
    {
        var readDtos = Meetups.Select(meetup => new ReadMeetupDto
        {
            Id = meetup.Id,
            Duration = meetup.Duration,
            Topic = meetup.Topic,
            Place = meetup.Place
        });

        return Ok(readDtos);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateMeetup([FromRoute] Guid id, [FromBody] UpdateMeetupDto updatedMeetup)
    {
        var oldMeetup = Meetups.SingleOrDefault(meetup => meetup.Id == id);

        // meetup with provided id does not exist
        if (oldMeetup is null)
        {
            return NotFound();
        }

        oldMeetup.Topic = updatedMeetup.Topic;
        oldMeetup.Place = updatedMeetup.Place;
        oldMeetup.Duration = updatedMeetup.Duration;
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteMeetup([FromRoute] Guid id)
    {
        var meetupToDelete = Meetups.SingleOrDefault(meetup => meetup.Id == id);

        // meetup with provided id does not exist
        if (meetupToDelete is null)
        {
            return NotFound();
        }

        Meetups.Remove(meetupToDelete);
        var readDto = new ReadMeetupDto
        {
            Id = meetupToDelete.Id,
            Topic = meetupToDelete.Topic,
            Place = meetupToDelete.Place,
            Duration = meetupToDelete.Duration
        };
        return Ok(readDto);
    }
    
}