import API from './api';

const authService = {
  login: async (username: string, password: string): Promise<string> => {
    const response = await fetch('http://localhost:5000/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ Username: username, Password: password }),
    });

    if (!response.ok) {
      throw new Error('Login inválido');
    }

    const data = await response.json();
    return data.token;
  },

  logout: () => {
    localStorage.removeItem('token');
  },

  getToken: () => {
    return localStorage.getItem('token');
  },
};


export default authService;
