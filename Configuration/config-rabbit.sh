echo "Iniciando configuração do RabbitMQ"
./rabbitmqadmin -u $user -p $password -H $host declare exchange name=book-entry type=fanout
./rabbitmqadmin -u $user -p $password -H $host declare queue name=BookEntryTopicTrigger
./rabbitmqadmin -u $user -p $password -H $host declare binding source=book-entry destination=BookEntryTopicTrigger
echo "Configuração do RabbitMQ Finalizada"