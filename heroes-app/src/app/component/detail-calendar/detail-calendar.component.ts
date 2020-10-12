import { HeroService } from './../../service/hero.service';
import {
  ChangeDetectionStrategy,
  Component,
  ViewChild,
  TemplateRef,
  Input,
  ChangeDetectorRef,
} from '@angular/core';
import * as moment from 'moment-timezone';
import { RRule, RRuleSet, rrulestr } from 'rrule';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { isSameDay, isSameMonth } from 'date-fns';
import { ViewPeriod } from 'calendar-utils';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView,
} from 'angular-calendar';

import { Constants } from '../../utils/constants';

import { Hero } from 'src/app/models/hero';

interface RecurringEvent {
  heroId: number;
  title: string;
  color: any;
  rrule: RRule;
}

interface TitleDate {
  title: string;
  date: moment.Moment;
  heroId: number;
}

@Component({
  selector: 'app-detail-calendar',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './detail-calendar.component.html',
  styleUrls: ['./detail-calendar.component.scss'],
})
export class DetailCalendarComponent {
  @Input() heroId: number;
  @Input() heroRole: string;
  @Input() hero: Hero;

  @ViewChild('modalContent', { static: true }) modalContent: TemplateRef<any>;

  public view: CalendarView = CalendarView.Month;
  public CalendarView = CalendarView;
  public viewDate: Date = new Date();
  public heroes: Hero[];
  public modalData: {
    action: string;
    event: CalendarEvent;
  };
  public isAlreadyChecked = false;
  public viewPeriod: ViewPeriod;

  public recurringEvents: RecurringEvent[] = [];

  public dates: TitleDate[] = [];

  public actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events = this.events.filter((iEvent) => iEvent !== event);
        this.handleEvent('Deleted', event);
      },
    },
  ];

  public refresh: Subject<any> = new Subject();
  public events: CalendarEvent[] = [];

  public activeDayIsOpen: boolean = false;

  public closestBirthdayText: string = '';

  constructor(
    private modal: NgbModal,
    private heroService: HeroService,
    private cdr: ChangeDetectorRef
  ) {}

  public convertRecurringEventsToCalendarEvents(): void {
    this.convertHerosToCalendarEvent();
    this.events = this.dates.map((date: TitleDate) => {
      return {
        title: date.title,
        start: date.date.toDate(),
        allDay: true,
        id: date.heroId,
      };
    });
    this.refresh.next();
    this.cdr.detectChanges();
  }

  ngOnInit() {
    this.heroService.getAllPlayers().subscribe((heroes) => {
      this.heroes = heroes;
      this.heroes = heroes.filter(
        (hero) => hero.isBadGuy === this.hero.isBadGuy
      );
      this.convertRecurringEventsToCalendarEvents();
      this.getNextBirthday();
    });
  }

  convertHerosToCalendarEvent(): void {
    this.heroes.forEach((hero: Hero) => {
      let date = moment(hero.birthday);

      let name = hero.name + "'s";
      if (hero.id === this.heroId) {
        name = 'Your';
      }
      let recurringEvent: RecurringEvent = {
        heroId: hero.id,
        title: `${name} birthday`,
        color: Constants.COLORS_BIRTHDAY.yellow,
        rrule: new RRule({
          freq: RRule.YEARLY,
          bymonth: date.month() + 1,
          bymonthday: date.date() - 1,
          dtstart: date.toDate(),
          until: date.add(70, 'y').toDate(),
        }),
      };
      this.recurringEvents.push(recurringEvent);
    });

    this.recurringEvents.forEach((recEvent: RecurringEvent) => {
      const initialTitle = recEvent.title;

      const dates = recEvent.rrule.all();

      const hero = this.heroes.find((hero) => hero.id === recEvent.heroId);

      dates.forEach((day: Date) => {
        const age = this.getAge(moment(day), hero);
        const title =
          initialTitle +
          `\nDate: ${day.toDateString()}\nAge: ${age}\nName: ${
            hero.name
          }\nCurrent Location: (${hero.longitude},${hero.latitude})`;
        this.dates.push({
          title: title,
          date: moment(day.toString()),
          heroId: recEvent.heroId,
        });
      });
    });
  }

  public dayClicked({
    date,
    events,
  }: {
    date: Date;
    events: CalendarEvent[];
  }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  public eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...event,
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  public handleEvent(action: string, event: CalendarEvent): void {
    this.modalData = { event, action };
    this.modal.open(this.modalContent, { size: 'lg' });
  }

  public closeOpenMonthViewDay(): void {
    this.activeDayIsOpen = false;
  }

  public getNextBirthday() {
    const now = moment();
    let closest: any = Infinity;
    let mom;
    this.dates.forEach((current) => {
      let date = moment(current.date);
      if (date >= now && (date < moment(closest) || date < closest)) {
        closest = date;
        mom = date;
      }
    });
    const days = moment.duration(mom.diff(now)).asDays();

    if (days >= 1) {
      let difference = Math.ceil(days);
      this.closestBirthdayText = '';
      this.closestBirthdayText = `The next birthday is ${difference} day(s) away`;
    } else {
      let difference = Math.floor(moment.duration(mom.diff(now)).asHours());
      this.closestBirthdayText = '';
      this.closestBirthdayText = `The next birthday is ${difference} hour(s) away`;
    }
    this.cdr.detectChanges();
  }

  public getAge(currentDate: moment.Moment, hero: Hero) {
    const birthday = moment(hero.birthday);
    return Math.floor(moment.duration(currentDate.diff(birthday)).asYears());
  }
}
