# Introduction
CalendarApplication is a WPF program that helps you keep track of your montly tasks.

## Features
The program support the following features:
* **Add** a new entrie to the calendar
* View entries
* **remove** existing entries in the calendar
* **Update** existing entries in the calendar
* The program remember its state between use.


## How to use?
The program can either be started with the Visual Studio 2019 or run the  CalendarApplication.exe file. which is located in  CalendarApplication\CalendarApplication\bin\Debug\netcoreapp3.1\CalendarApplication.exe

When run for the first time the database will be poplated with mock data



## How Does It Work?
The program consists of three views:

**CalendarView** is the main view in this program. Here you can view and filter the calendar entries to match a spesific timefram. To add a new entrie you press **Add entrie** this will take you to the next view.

**CreateCalendarView** allows you to create new entries. Each entrie must have `Title`, `from-date`, `to-date`, and a `descrption`. When you press save, the entrie will automaticy be added to the datebase and uppdated in the **CalendarView**.

**UppdateCalendarView** allows you to update exsiting entries. Each entrie must have `Title`, `from-date`, `to-date`, and a `descrption`. When you press save, the entrie will automaticy be uppdated in the datebase and uppdated in the **CalendarView**.

## program architecture
This program is based on `Model-View-ViewModel(MVVM)` pattern.

Model-View-ViewModel (MVVM) is a structural design pattern that separates objects into three distinct groups:

* Models hold application data. They’re usually structs or simple classes.

* Views display visual elements and controls on the screen. 

* View models transform model information into values that can be displayed on a view. They’re usually classes, so they can be passed around as references.

this pattern makes it easy to test business logic.

This Program also uses dependency injection for the separation of Concerns

## Quality
The solution is verified by automated tests (Unit Tests).

## Technology
* The program is a `WPF`-program.
* `caliburn micro` is used for the mvvm design pattern. 
* The solution is written using `C#` as the primary language.
* `Xunit` and `Moq` are used for unit testing.

  