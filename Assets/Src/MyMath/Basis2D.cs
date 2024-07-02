using MyMath;

public struct Basis2D
{
    public Vec3 x;
    public Vec3 y;

    public Basis2D(Vec3 x, Vec3 y)
    {
        this.x = x;
        this.y = y;
    }

    public static Basis2D Default => new Basis2D(Vec3.Right, Vec3.Up);
}