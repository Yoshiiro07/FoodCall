export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  user: {
    id: string;
    name: string;
    email: string;
  };
}

export interface UserDto {
  id: string;
  name: string;
  email: string;
  phoneNumber?: string;
  address?: string;
}
