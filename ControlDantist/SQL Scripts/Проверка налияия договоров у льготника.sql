declare @FistName nvarchar(100)
                            declare @Name nvarchar(100)
                            declare @Surname nvarchar(100)
							declare @NumContract nvarchar(50)
                            declare @DR date
                            set @FistName = '�������' --'����������' --'��������' --'�������' 
                            set @Name = '������'-- '������' --'������' -- '����'
                            set @Surname = '����������'-- '��������'
                            set @DR = '19550501' --'19490903'-- '19440723'
							--set @NumContract = '����-1/227'
select �������,���,��������,�������.�������������,��������.������������,������������,[���������],�������.flag����������,�������.�������������� from ��������
inner join �������
on ��������.id_�������� = �������.id_��������
left outer join �������������������
on �������.id_������� = �������������������.id_�������
where ((LOWER(RTRIM(LTRIM([�������]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(���))) = LOWER(LTRIM(RTRIM(@Name))))
							and ((LOWER(LTRIM(RTRIM(��������))) = LOWER(LTRIM(RTRIM(@Surname)))) 
							--or LOWER(RTRIM(LTRIM([�������]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(���))) = LOWER(LTRIM(RTRIM(@Name))))
))
--) and LOWER(RTRIM(LTRIM(�������.�������������))) <> LOWER(RTRIM(LTRIM(@NumContract))))

--select COUNT(��������.id_��������), �������,���,��������,��������.������������ from ��������
--inner join �������
--on ��������.id_�������� = �������.id_��������
--group by  �������,���,��������,��������.������������
--having COUNT(��������.id_��������) > 3