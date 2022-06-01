declare @FistName nvarchar(100)
                            declare @Name nvarchar(100)
                            declare @Surname nvarchar(100)
							declare @NumContract nvarchar(50)
                            declare @DR date
                            set @FistName = 'Маркова' --'Григорьева' --'Аккулова' --'Фадеева' 
                            set @Name = 'Любовь'-- 'Галина' --'Казира' -- 'Нина'
                            set @Surname = 'Николаевна'-- 'Ивановна'
                            set @DR = '19550501' --'19490903'-- '19440723'
							--set @NumContract = 'СМСП-1/227'
select Фамилия,Имя,Отчество,Договор.НомерДоговора,Льготник.ДатаРождения,ДатаДоговора,[НомерАкта],Договор.flagАнулирован,Договор.ФлагАнулирован from Льготник
inner join Договор
on Льготник.id_льготник = Договор.id_льготник
left outer join АктВыполненныхРабот
on Договор.id_договор = АктВыполненныхРабот.id_договор
where ((LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name))))
							and ((LOWER(LTRIM(RTRIM(Отчество))) = LOWER(LTRIM(RTRIM(@Surname)))) 
							--or LOWER(RTRIM(LTRIM([Фамилия]))) = LOWER(LTRIM(RTRIM(@FistName))) and LOWER(LTRIM(RTRIM(Имя))) = LOWER(LTRIM(RTRIM(@Name))))
))
--) and LOWER(RTRIM(LTRIM(Договор.НомерДоговора))) <> LOWER(RTRIM(LTRIM(@NumContract))))

--select COUNT(Льготник.id_льготник), Фамилия,Имя,Отчество,Льготник.ДатаРождения from Льготник
--inner join Договор
--on Льготник.id_льготник = Договор.id_льготник
--group by  Фамилия,Имя,Отчество,Льготник.ДатаРождения
--having COUNT(Льготник.id_льготник) > 3