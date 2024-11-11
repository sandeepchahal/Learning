export class User {
  constructor(
    public username: string,
    public email: string,
    public password: string,
    public confirmPassword?: string // Add confirmPassword property
  ) {}
}
