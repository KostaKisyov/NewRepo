using System;
using System.Collections.Generic;

// Command Pattern
// Command Interface
public interface ICommand
{
    void Execute();
}

// Concrete Command
public class LieDownCommand : ICommand
{
    private Trainee trainee;

    public LieDownCommand(Trainee trainee)
    {
        this.trainee = trainee;
    }

    public void Execute()
    {
        trainee.LieDown();
    }
}

public class StandUpCommand : ICommand
{
    private Trainee trainee;

    public StandUpCommand(Trainee trainee)
    {
        this.trainee = trainee;
    }

    public void Execute()
    {
        trainee.StandUp();
    }
}

// Receiver
public class Trainee
{
    public void LieDown()
    {
        Console.WriteLine("Trainee is lying down.");
    }

    public void StandUp()
    {
        Console.WriteLine("Trainee is standing up.");
    }
}

// Invoker
public class FitnessInstructor
{
    private ICommand command;

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    public void IssueCommand()
    {
        command.Execute();
    }
}

// Observer Pattern
// Subject
public class FitnessSession
{
    private List<IObserver> observers = new List<IObserver>();
    private string currentAction;

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void SetAction(string action)
    {
        currentAction = action;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(currentAction);
        }
    }
}

// Observer Interface
public interface IObserver
{
    void Update(string action);
}

// ConcreteObserver
public class OnlineViewer : IObserver
{
    private string name;

    public OnlineViewer(string name)
    {
        this.name = name;
    }

    public void Update(string action)
    {
        Console.WriteLine($"{name} is now {action}.");
    }
}

// Main Program to Demonstrate
public class Program
{
    public static void Main(string[] args)
    {
        // Command Pattern Example
        Trainee trainee = new Trainee();
        ICommand lieDown = new LieDownCommand(trainee);
        ICommand standUp = new StandUpCommand(trainee);

        FitnessInstructor instructor = new FitnessInstructor();
        instructor.SetCommand(lieDown);
        instructor.IssueCommand();

        instructor.SetCommand(standUp);
        instructor.IssueCommand();

        // Observer Pattern Example
        FitnessSession session = new FitnessSession();

        // Adding online viewers
        OnlineViewer viewer1 = new OnlineViewer("Viewer 1");
        OnlineViewer viewer2 = new OnlineViewer("Viewer 2");
        OnlineViewer viewer3 = new OnlineViewer("Viewer 3");

        session.AddObserver(viewer1);
        session.AddObserver(viewer2);
        session.AddObserver(viewer3);

        // Simulate instructor's action
        session.SetAction("lying down");
        session.SetAction("standing up");
    }
}
