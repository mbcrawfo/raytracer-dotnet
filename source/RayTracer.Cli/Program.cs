using System;
using System.Threading;
using Tuple = RayTracer.Core.Tuple;

var env = new Environment(Tuple.Vector(0, -0.1, 0), Tuple.Vector(-0.01, 0, 0));

var proj = new Projectile(Tuple.Point(0, 1, 0), Tuple.Vector(1, 1, 0).Normalize());

var tick = 0;
while (proj.Position.Y >= 0.0)
{
    proj = proj.Tick(env);
    Console.WriteLine($"Tick {tick++}: {proj}");
    Thread.Sleep(500);
}

internal record Environment(Tuple Gravity, Tuple Wind);

internal record Projectile(Tuple Position, Tuple Velocity)
{
    public Projectile Tick(Environment environment)
    {
        var (gravity, wind) = environment;
        return this with { Position = Position + Velocity, Velocity = Velocity + gravity + wind };
    }
}

