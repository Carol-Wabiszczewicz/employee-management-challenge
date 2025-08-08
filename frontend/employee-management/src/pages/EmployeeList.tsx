import { useEffect, useState } from 'react';
import {
  getEmployees,
  createEmployee,
  updateEmployee,
  deleteEmployee
} from '../services/employeeService';
import EmployeeTable from '../components/EmployeeTable/EmployeeTable';
import EmployeeFormModal from '../components/EmployeeTable/EmployeeFormModal';
import '../styles/EmployeeList.css';

export type Employee = {
  id: number;
  fullName: string;
  email: string;
  birthDate: string;
  docNumber: string;
  phones: string[];
  position: string;
  salary: number;
  managerId: number | null;
};

export default function EmployeeList() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [showModal, setShowModal] = useState(false);
  const [employeeBeingEdited, setEmployeeBeingEdited] = useState<Employee | null>(null);

  const fetchEmployees = () => {
    getEmployees()
      .then(response => setEmployees(response.data.items))
      .catch(error => console.error('Erro ao carregar funcionários:', error));
  };

  useEffect(() => {
    fetchEmployees();
  }, []);

  const handleAddClick = () => {
    setEmployeeBeingEdited(null);
    setShowModal(true);
  };

  const handleFormSubmit = (employee: Omit<Employee, 'id'>) => {
    if (employeeBeingEdited) {
      updateEmployee(employeeBeingEdited.id, employee).then(() => {
        fetchEmployees();
        setShowModal(false);
        setEmployeeBeingEdited(null);
      });
    } else {
      createEmployee(employee).then(() => {
        fetchEmployees();
        setShowModal(false);
      });
    }
  };

  const handleDeleteEmployee = (id: number) => {
    if (!window.confirm('Tem certeza que deseja excluir este funcionário?')) return;

    deleteEmployee(id.toString())
      .then(() => fetchEmployees())
      .catch((err) => {
        console.error('Erro ao excluir funcionário:', err);
        alert('Erro ao excluir. Verifique o console.');
      });
  };

  const handleEditEmployee = (employee: Employee) => {
    setEmployeeBeingEdited(employee);
    setShowModal(true);
  };

  return (
    <div className="employee-list-container">
      <h2>Lista de Funcionários</h2>
      <button className="add-btn" onClick={handleAddClick}>
        + Adicionar Funcionário
      </button>
      <EmployeeTable
        employees={employees}
        onDelete={handleDeleteEmployee}
        onEdit={handleEditEmployee}
      />

      {showModal && (
        <EmployeeFormModal
          onClose={() => {
            setShowModal(false);
            setEmployeeBeingEdited(null);
          }}
          onSubmit={handleFormSubmit}
          employee={employeeBeingEdited}
        />
      )}
    </div>
  );
}
