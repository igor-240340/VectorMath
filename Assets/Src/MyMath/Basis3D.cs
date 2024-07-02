using MyMath;

public class Basis3D
{
    public Vec3 x;
    public Vec3 y;
    public Vec3 z;

    public Basis3D(Vec3 x, Vec3 y, Vec3 z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Basis3D Standard => new Basis3D(Vec3.Right, Vec3.Up, Vec3.Forward);
}