export interface User {
  id: number;
  name: string;
  email: string;
  createdAt: string;
}

export interface CreateUser {
  name: string;
  email: string;
}
