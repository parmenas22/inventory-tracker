export interface DecodedToken {
  FirstName: string;
  LastName: string;
  UserId: string;
  email: string;
  iss: string;
  jti: string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"?:
    | string
    | string[];
  role?: string | string[];
  roles?: string[];
}

export const getUserRoles = (user: DecodedToken | null): string[] => {
  const roleClaim =
    user?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ??
    user?.role ??
    user?.roles ??
    [];

  return (Array.isArray(roleClaim) ? roleClaim : [roleClaim]).filter(
    (role): role is string => typeof role === "string"
  );
};
