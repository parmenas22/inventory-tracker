import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { toast } from "sonner";
import { useNavigate } from "react-router-dom";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { authService } from "@/services/auth.service";
import { registerSchema, type RegisterSchema } from "@/validators/registerSchema";

const Signup = () => {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterSchema>({
    resolver: zodResolver(registerSchema),
  });

  const onSubmit = async (data: RegisterSchema) => {
    const res = await authService.register({
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      password: data.password,
    });

    if (res?.succeeded) {
      toast.success(res.message);
      navigate("/login");
      return;
    }

    toast.error(res?.message ?? "Something went wrong");
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-linear-to-br from-green-50 via-white to-neutral-50 p-4">
      <Card className="w-full max-w-md shadow-sm border border-green-100">
        <CardHeader className="space-y-2 text-center">
          <CardTitle className="text-2xl font-semibold">Create an account</CardTitle>
          <CardDescription className="text-neutral-600">
            Register with your email, first name, and last name.
          </CardDescription>
        </CardHeader>

        <CardContent className="space-y-4">
          <form className="space-y-8" onSubmit={handleSubmit(onSubmit)}>
            <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
              <div className="space-y-2">
                <Label htmlFor="firstName">First Name</Label>
                <Input
                  id="firstName"
                  placeholder="Jane"
                  className="border-green-200 focus:border-green-400"
                  {...register("firstName")}
                />
                {errors.firstName && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.firstName.message}
                  </p>
                )}
              </div>

              <div className="space-y-2">
                <Label htmlFor="lastName">Last Name</Label>
                <Input
                  id="lastName"
                  placeholder="Doe"
                  className="border-green-200 focus:border-green-400"
                  {...register("lastName")}
                />
                {errors.lastName && (
                  <p className="text-red-500 text-sm mt-1">
                    {errors.lastName.message}
                  </p>
                )}
              </div>
            </div>

            <div className="space-y-2">
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                type="email"
                placeholder="name@example.com"
                className="border-green-200 focus:border-green-400"
                {...register("email")}
              />
              {errors.email && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.email.message}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <Label htmlFor="password">Password</Label>
              <Input
                id="password"
                type="password"
                placeholder="••••••••"
                className="border-green-200 focus:border-green-400"
                {...register("password")}
              />
              {errors.password && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.password.message}
                </p>
              )}
            </div>

            <div className="space-y-2">
              <Label htmlFor="confirmPassword">Confirm Password</Label>
              <Input
                id="confirmPassword"
                type="password"
                placeholder="••••••••"
                className="border-green-200 focus:border-green-400"
                {...register("confirmPassword")}
              />
              {errors.confirmPassword && (
                <p className="text-red-500 text-sm mt-1">
                  {errors.confirmPassword.message}
                </p>
              )}
            </div>

            <Button
              type="submit"
              className="w-full bg-green-500 hover:bg-green-600 text-white"
            >
              Sign Up
            </Button>
          </form>

          <div className="text-center text-sm mt-4">
            <button
              type="button"
              onClick={() => navigate("/login")}
              className="text-green-600 hover:text-green-700 font-medium transition-colors"
            >
              Already have an account? Back to login
            </button>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};

export default Signup;
