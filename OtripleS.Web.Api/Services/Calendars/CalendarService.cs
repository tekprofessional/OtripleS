﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using OtripleS.Web.Api.Brokers.DateTimes;
using OtripleS.Web.Api.Brokers.Loggings;
using OtripleS.Web.Api.Brokers.Storage;
using OtripleS.Web.Api.Models.Calendars;

namespace OtripleS.Web.Api.Services.Calendars
{
	public partial class CalendarService : ICalendarService
	{
		private readonly IStorageBroker storageBroker;
		private readonly ILoggingBroker loggingBroker;
		private readonly IDateTimeBroker dateTimeBroker;

		public CalendarService(IStorageBroker storageBroker,
			ILoggingBroker loggingBroker,
			IDateTimeBroker dateTimeBroker)
		{
			this.storageBroker = storageBroker;
			this.loggingBroker = loggingBroker;
			this.dateTimeBroker = dateTimeBroker;
		}

		public async ValueTask<Calendar> ModifyCalendarAsync(Calendar calendar)
		{
			Calendar maybeCalendar =
			   await this.storageBroker.SelectCalendarByIdAsync(calendar.Id);

			dateTimeBroker.GetCurrentDateTime();

			return await this.storageBroker.UpdateCalendarAsync(calendar);
		}

		public ValueTask<Calendar> RetrieveCalendarByIdAsync(Guid calendarId) =>
		TryCatch(async () =>
		{
			ValidateCalendarId(calendarId);
			Calendar storageCalendar = await this.storageBroker.SelectCalendarByIdAsync(calendarId);
			ValidateStorageCalendar(storageCalendar, calendarId);

			return storageCalendar;
		});
	}
}
