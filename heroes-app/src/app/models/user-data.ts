export class UserData {
  public id: number;
  public fullName: string;
  public username: string;
  public email: string;
  public password: string;
  public confirmationPassword: string;
  public avatarPath: string;
  public token?: string;
  public age: number;
  public role: string;
}

export enum EntityType {
  Hero = 'Hero',
  Villain = 'Villain',
}
