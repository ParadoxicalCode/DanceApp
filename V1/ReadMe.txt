1) Импортировать базу данных в SSMS
2) Удалить модель ADO.NET
3) Создать модель ADO.NET
4) Назвать модель SkatingModel
5) Сохранить параметры соединения в App.Config как: SkatingEntities

В файле SkatingModel.Context.cs, который находится по пути SkatingModel.edmx > SkatingModel.Context.tt прописать:
private static SkatingEntities _context;
public static SkatingEntities GetContext()
{
    if (_context == null)
        _context = new SkatingEntities();
    return _context;
}