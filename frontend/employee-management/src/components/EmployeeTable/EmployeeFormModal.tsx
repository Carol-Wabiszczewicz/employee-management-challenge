import { useEffect } from 'react';
import '../../styles/EmployeeFormModal.css';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { employeeSchema } from '../../validations/employeeSchema';

const roleOptions = [
  { label: 'Analista', value: 'Analyst' },
  { label: 'Desenvolvedor', value: 'Developer' },
  { label: 'Estagiário', value: 'Intern' },
  { label: 'Gerente', value: 'Manager' },
];

interface EmployeeFormValues {
  fullName: string;
  email: string;
  birthDate: string;
  docNumber: string;
  phones: string[];
  position: string;
  salary: number;
  managerId: number | null;
}

interface Employee {
  id: number;
  fullName: string;
  email: string;
  birthDate: string;
  docNumber: string;
  phones: string[];
  position: string;
  salary: number;
  managerId: number | null;
}

interface EmployeeFormModalProps {
  onSubmit: (data: EmployeeFormValues) => void;
  onClose: () => void;
  employee?: Employee | null;
}

export default function EmployeeFormModal({ onSubmit, onClose, employee }: EmployeeFormModalProps) {
  const {
    register,
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<EmployeeFormValues>({
    resolver: zodResolver(employeeSchema),
    defaultValues: {
      fullName: '',
      email: '',
      birthDate: '',
      docNumber: '',
      phones: [''],
      position: '',
      salary: 0,
      managerId: null,
    },
  });

  useEffect(() => {
    if (employee) {
      reset({
        ...employee,
        birthDate: employee.birthDate?.slice(0, 10),
        phones: employee.phones.length ? employee.phones : [''],
      });
    }
  }, [employee, reset]);

  const submitForm = (data: EmployeeFormValues) => {
    onSubmit({
      ...data,
      birthDate: new Date(data.birthDate).toISOString(),
    });
  };

  function formatCPF(value: string) {
    const cleaned = value.replace(/\D/g, '').slice(0, 11); 

    const formatted = cleaned
      .replace(/^(\d{3})(\d)/, '$1.$2')
      .replace(/^(\d{3})\.(\d{3})(\d)/, '$1.$2.$3')
      .replace(/^(\d{3})\.(\d{3})\.(\d{3})(\d)/, '$1.$2.$3-$4');

    return formatted;
  }


  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h3>{employee ? 'Editar Funcionário' : 'Adicionar Funcionário'}</h3>
        <form onSubmit={handleSubmit(submitForm)} className="modal-form">
          <input placeholder="Nome completo" {...register('fullName')} />
          {errors.fullName && <span>{errors.fullName.message}</span>}

          <input type="email" placeholder="Email" {...register('email')} />
          {errors.email && <span>{errors.email.message}</span>}

          <input type="date" placeholder="Data de nascimento" {...register('birthDate')} />
          {errors.birthDate && <span>{errors.birthDate.message}</span>}

         <input
          {...register("docNumber")}
          maxLength={14}
          placeholder="000.000.000-00"
        />
        {errors.docNumber && <span>{errors.docNumber.message}</span>}

          <input
            type="text"
            placeholder="(00) 1111-1111"
            maxLength={12}
            {...register("docNumber", {
              onChange: (e) => {
                e.target.value = e.target.value.replace(/[^\d.-]/g, '');
              },
            })}
          />

          {errors.phones?.[0] && <span>{errors.phones[0].message}</span>}

          <select {...register('position')}>
            <option value="">Selecione o cargo</option>
            {roleOptions.map((opt) => (
              <option key={opt.value} value={opt.value}>{opt.label}</option>
            ))}
          </select>
          {errors.position && <span>{errors.position.message}</span>}

          <input type="number" step="0.01" placeholder="Salário" {...register('salary')} />
          {errors.salary && <span>{errors.salary.message}</span>}

          <input type="number" placeholder="ID do Gerente (opcional)" {...register('managerId', { valueAsNumber: true })} />

          <div className="modal-buttons">
            <button type="submit" className="save-btn">Salvar</button>
            <button type="button" className="cancel-btn" onClick={onClose}>Cancelar</button>
          </div>
        </form>
      </div>
    </div>
  );
}
