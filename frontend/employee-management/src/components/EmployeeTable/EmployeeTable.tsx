import React from 'react';
import '../../styles/EmployeeTable.css';
import { Employee } from '../../pages/EmployeeList';


interface EmployeeTableProps {
  employees: Employee[];
  onDelete: (id: number) => void; 
  onEdit: (employee: Employee) => void;
}


const EmployeeTable = ({ employees, onDelete, onEdit }: EmployeeTableProps) => {
  return (
    <table className="employee-table">
      <thead>
        <tr>
          <th>Nome</th>
          <th>Email</th>
          <th>Cargo</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        {employees.map((emp) => (
          <tr key={emp.id}>
            <td>{emp.fullName}</td>
            <td>{emp.email}</td>
            <td>{emp.position}</td>
            <td>
              <button onClick={() => onEdit(emp)} className="edit-btn">Editar</button>
              <button onClick={() => onDelete(emp.id)} className="delete-btn">Excluir</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};


export default EmployeeTable;
