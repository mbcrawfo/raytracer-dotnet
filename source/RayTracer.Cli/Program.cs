using System;
using System.Threading;
using RayTracer.Core.Math;

var env = new Environment(new Vector(0f, -0.1f, 0f), new Vector(-0.01f, 0f, 0f));
var proj = new Projectile(new Point(0f, 1f, 0f), new Vector(1f, 1f, 0f).Normalize());
var tick = 0;

while (proj.Position.Y >= 0.0)
{
    proj = proj.Tick(env);
    Console.WriteLine($"Tick {tick++}: {proj}");
    Thread.Sleep(500);
}

return 0;

internal record Environment(Vector Gravity, Vector Wind);

internal record Projectile(Point Position, Vector Velocity)
{
    public Projectile Tick(Environment environment) =>
        this with
        {
            Position = Position + Velocity,
            Velocity = Velocity + environment.Gravity + environment.Wind
        };
}
