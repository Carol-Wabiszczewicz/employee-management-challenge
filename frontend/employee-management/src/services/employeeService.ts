import API from './api';

export const getEmployees = () => API.get('/employees');

export const createEmployee = (employee: {
  fullName: string;
  email: string;
  birthDate: string;
  docNumber: string;
  phones: string[];
  position: string;
  salary: number;
  managerId: number | null;
}) => {
  return API.post('/employees', employee);
};

export const updateEmployee = (id: number, employee: {
  fullName: string;
  email: string;
  birthDate: string;
  docNumber: string;
  phones: string[];
  position: string;
  salary: number;
  managerId: number | null;
}) => {
  return API.put(`/employees/${id}`, employee);
};

export const deleteEmployee = (id: string) => API.delete(`/employees/${id}`);
