# Вариант 2 - Система контроля версий текста.

### Описание

Система контроля версий текста. Как пример – история изменений статей на википедии, или тот же git. В системе содержатся статьи, их можно читать, изменять и оставлять комментарии. Изменения можно также комментировать, и откатывать.

### Модели

Статья:
- Название
- Автор статьи
- Последняя версия текста
- История изменений текста

История изменений:
- Изменение
- Дата и время изменения
- Автор изменения
- Предыдущее изменение
- Следующее изменение

Комментарий к статье:
- Текст комментария
- Автор комментария

Комментарий к изменению:
- Текст комментария
- Автор комментария

Пользователь:
- Логин
- Пароль
- Роль

### Роли

- User - зарегистрированный пользователь.
- Editor - пользователь с возможностью создавать или редактировать статьи.
- Moderator - пользователь с возможностью удалять статьи и откатывать изменения в случае, если этого не могут (не хотят) делать авторы статьи или изменения соответственно.

### API Методы

1. `POST /login` - Проверяется логин и пароль, если все успешно то используется авторизация через JWT и проставляется роль из БД. Для тестирования вручную добавьте примеры пользователей через HasData в DbContext.
2. `GET /` - список статей. Доступно всем.
- `POST /` - добавить статью. Доступно только пользователям с ролью Editor.
- `GET /{articleId}` - просмотр последней версии статьи. Доступно всем.
- `PUT /{articleId}` - добавление изменения в текст статьи. Название и автор статьи не подлежат изменению. Доступно только пользователям с ролью Editor.
- `DELETE /{articleId}` - удаление статьи. Доступно только автору статьи, или пользователям с ролью Moderator.
- `GET /{articleId}/comments` - просмотр комментариев к статье. Доступно всем.
- `POST /{articleId}/comments` - добавление комментария к статье. Доступно только зарегистрированным пользователям.
- `GET /{articleId}/revisions` - список краткой информации об изменениях, без их содержания, но с комментариями к изменениям. Доступно всем.
- `GET /{articleId}/revisions/{revisionId}` - просмотр статьи на момент изменения номер `revisionId`. Доступно всем.
- `GET /{articleId}/revisions/compare?{firstRevisionId}&{secondRevisionId}` - сравнение версий статьи от `firstRevisionId` и от `secondRevisionId`. Формат вывода может быть любой, популярным является [unidiff](https://ru.wikipedia.org/wiki/Diff#Универсальный_формат). Главное чтобы было видно, как из `firstRevisionId` получить `secondRevisionId`. Доступно всем.
- `POST /{articleId}/revisions/rollback` - возвращение к предыдущей версии статьи. Последнее изменение удаляется из памяти. Доступно только автору откатываемого изменения, автору статьи или пользователю с ролью Moderator.

### Примечания к реализации

Это контрольная по ОРИС, а не по АиСД, поэтому в задачи не входит оптимизация хранения истории изменений, то есть можно просто на каждое изменение хранить полный текст. То же самое касается и сравнений двух версий, самым простым будет такой ответ (в формате [unidiff](https://ru.wikipedia.org/wiki/Diff#Универсальный_формат)):

```diff
@@ -1,{количество строк в первом файле} +1,{количество строк во втором} @@
-[Все строки первого файла
-с одним минусом слева]
+[Все строки второго файла
+с одним плюсом слева]
```

Если все-таки хочется заморочиться, то есть [хорошо задокументированный исходный код реализации diff на C#](https://github.com/mathertel/Diff), готовые библиотеки ([DiffPlex](https://www.nuget.org/packages/DiffPlex)), или сторонние API ([Diffchecker](https://www.diffchecker.com/public-api/)). В хорошо спроектированной архитектуре реализации сервисов, использующие тот или иной метод, должны быть взаимозаменяемы.